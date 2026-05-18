using System;
using System.Collections.Generic;
using ScriptPortal.Vegas;
using NAudio.Wave;
using System.Windows.Forms;

namespace SuperBunnyCut
{
    public partial class AudioSplitter : Form
    {
        public bool sampleMedia = false;

        public AudioSplitter()
        {
            InitializeComponent();

            volThreshold.Value      = (decimal)EntryPoint.volumeThreshold * 100;
            pauseTimeMin.Value      = (decimal)EntryPoint.pauseTimeMin;
            SplitPointNum.Value     = (decimal)EntryPoint.splitPoint * 100;
        }

        private void processAudio_Click(object sender, EventArgs e)
        {
            //Set our values
            EntryPoint.volumeThreshold = (float)volThreshold.Value / 100;
            EntryPoint.pauseTimeMin = (float)pauseTimeMin.Value;
            EntryPoint.splitPoint = (float)SplitPointNum.Value / 100;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void volThreshold_ValueChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void mediaSampleBtn_Click(object sender, EventArgs e)
        {
            sampleMedia = true;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void SplitPointNum_ValueChanged(object sender, EventArgs e)
        {

        }
    }

    public class EntryPoint
    {
        public static float volumeThreshold = 0.01f;
        public static float pauseTimeMin = 0.2f;  //1/5th Second
        public static float splitPoint = 0.5f; //50%

        public void FromVegas(Vegas vegas)
        {
            using (AudioSplitter splitter = new AudioSplitter())
            {
                DialogResult result = splitter.ShowDialog();
                if (result == DialogResult.OK)
                {
                    try
                    {
                        if (splitter.sampleMedia)
                            SampleMedia(vegas);
                        else
                            SplitAtSilence(vegas);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error:\n" + e, "ERROR");
                    }
                }
            }
        }

        void SampleMedia(Vegas vegas)
        {
            TrackEvent[] selectedEvents = Global.GetSelectedEvents(vegas.Project);
            if (selectedEvents.Length == 0)
                return;

            for (int i = 0; i < selectedEvents.Length; i++)
            {
                AudioEvent track = selectedEvents[i] as AudioEvent;
                float[] mediaSamples = Global.ReadAudioSamples(track.ActiveTake.Media.FilePath);
                float samplesPerMillisecond = (float)(mediaSamples.Length / Global.totalTime);
                double sampleLength = track.Length.ToMilliseconds() * samplesPerMillisecond;
                int minPauseSamples = (int)((pauseTimeMin * 1000) * samplesPerMillisecond);

                double millisecondsPerSample = (Global.totalTime / mediaSamples.Length);

                int startSample = (int)(track.ActiveTake.Offset.ToMilliseconds() * samplesPerMillisecond);
                int endSample = startSample + (int)(track.Length.ToMilliseconds() * samplesPerMillisecond);

                MessageBox.Show("Start " + track.Start.ToMilliseconds()
                    + "\nEnd: " + track.End.ToMilliseconds()
                    + "\nLength: " + track.Length.ToMilliseconds()
                    + "\nTakeOffset: " + track.ActiveTake.Offset.ToMilliseconds()
                    + "\nSamples: " + mediaSamples.Length
                    + "\nSample Start: " + startSample
                    + "\nSample End: " + endSample
                    + "\nSamples Per Millisecond: " + samplesPerMillisecond
                    + "\nSample Length: " + sampleLength
                    + "\nSample Min Pause: " + minPauseSamples
                    + "\nSample Estimate Total(ms): " + samplesPerMillisecond * track.Length.ToMilliseconds()
                    , "Media Data");
            }
        }

        void SplitAtSilence(Vegas vegas)
        {
            TrackEvent[] selectedEvents = Global.GetSelectedEvents(vegas.Project);
            if (selectedEvents.Length == 0)
                return;

            List<PauseRange> pauses = new List<PauseRange>();

            for (int i = 0; i < selectedEvents.Length; i++)
            {
                AudioEvent track = selectedEvents[i] as AudioEvent;
                float[] mediaSamples = Global.ReadAudioSamples(track.ActiveTake.Media.FilePath);
                float samplesPerMillisecond = (float)(mediaSamples.Length / Global.totalTime);
                double sampleLength = track.Length.ToMilliseconds() * samplesPerMillisecond;
                int minPauseSamples = (int)((pauseTimeMin * 1000) * samplesPerMillisecond);


                double millisecondsPerSample = (Global.totalTime / mediaSamples.Length);

                int startSample = (int)(track.ActiveTake.Offset.ToMilliseconds() * samplesPerMillisecond);
                int endSample = startSample + (int)(track.Length.ToMilliseconds() * samplesPerMillisecond);

                bool isPaused = false;
                PauseRange pauseTracker = new PauseRange();

                //Edge Check
                if(endSample > mediaSamples.Length)
                    endSample = mediaSamples.Length;

                for (int x = startSample; x < endSample; x++)
                {
                    if (mediaSamples[x] <= volumeThreshold)
                    {
                        //Get our start position
                        if (isPaused == false)
                        {
                            isPaused = true;
                            pauseTracker.startIndex = x;
                        }
                        else if (x == endSample - 1 && pauseTracker.startIndex != 0)
                        {
                            //End of Track, force cut
                            pauseTracker.endIndex = x;
                            //pauses.Add(new PauseRange(startIndex, endIndex));
                            pauses.Add(new PauseRange(pauseTracker.startIndex, pauseTracker.endIndex));
                            pauseTracker.startIndex = 0;
                            pauseTracker.endIndex = 0;
                        }
                        continue;
                    }
                    else
                    {
                        if (isPaused)
                        {
                            isPaused = false;
                            //Within Threshold
                            if (x - pauseTracker.startIndex >= minPauseSamples)
                            {
                                pauseTracker.endIndex = x;
                                pauses.Add(new PauseRange(pauseTracker.startIndex, pauseTracker.endIndex));
                                pauseTracker.startIndex = 0;
                                pauseTracker.endIndex = 0;
                                continue;
                            }
                            else
                            {
                                //Reset
                                pauseTracker.startIndex = 0;
                                pauseTracker.endIndex = 0;
                            }
                        }
                    }
                }

                //Cut in Reverse
                for (int x = pauses.Count - 1; x >= 0; x--)
                {
                    //End of File or no more silence
                    if (pauses[x].startIndex == -1 || pauses[x].endIndex == -1)
                        continue;

                    int indexLength = (pauses[x].endIndex - pauses[x].startIndex);
                    //Calculate pause Length
                    int indexSplitPoint = (int)(indexLength * splitPoint);

                    //Location of the total Media Samples we want to clip at
                    int absoluteIndex = pauses[x].startIndex + indexSplitPoint;

                    //The point on the entire clip we want to cut at
                    double absoluteTimeSplit = absoluteIndex * millisecondsPerSample;

                    //Cut the track offset time to where we are doing our checks
                    double offsetSplit = absoluteTimeSplit - (track.ActiveTake.Offset.ToMilliseconds());

                    Timecode splitAt = new Timecode(offsetSplit);
                    track.Split(splitAt);

                    /*
                    MessageBox.Show("SPLIT: " + splitAt.ToMilliseconds()
                        + "\n index: " + indexSplitPoint
                        + "\n samleLEn: " + sampleLength);
                    */
                }
            }
        }
    }
}
