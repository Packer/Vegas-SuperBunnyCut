
using System.Windows.Forms;
using System.Collections.Generic;
//using Sony.Vegas;
using ScriptPortal.Vegas;
using NAudio.Wave;

namespace SuperBunnyCut
{

    public class EntryPoint
    {
        public float volumeThreshold = 0.05f;
        public float pauseTimeMin = 1;  //1 Second

        public void FromVegas(Vegas vegas)
        {
            //MessageBox.Show(vegas.Version);
            //MessageBox.Show(vegas.Project.Length.ToString());

            try
            {
                TrackEvent[] selectedEvents = GetSelectedEvents(vegas.Project);
                if (selectedEvents.Length == 0)
                    return;

                int startArrayIndex = 0;
                double startTrack = 0;
                for (int i = 0; i < selectedEvents.Length; i++)
                {
                    //MessageBox.Show(selectedEvents[i].MediaType.ToString());
                    //MessageBox.Show(selectedEvents[i].ActiveTake.Media.FilePath);

                    //Track our current focused Track when we cut it up
                    TrackEvent currentTrack = selectedEvents[i];

                    while (true)
                    {
                        AudioEvent track = currentTrack as AudioEvent;
                        Timecode trackLength = track.Length;
                        MessageBox.Show("Start " + track.Start + " End: " + track.End + " Length: " + track.Length + " SyncOffset: " + track.SyncOffset + " TakeOffset: "+ track.ActiveTake.Offset);
                        break;
                        float[] array = ReadAudioSamples(currentTrack.ActiveTake.Media.FilePath);
                        

                        MessageBox.Show("Track is " + trackLength.ToMilliseconds() + " Milliseconds long and array: " + array.Length);

                        double sampleLength = trackLength.ToMilliseconds() / array.Length;

                        List<PauseRange> pauses = new List<PauseRange>();

                        bool isPaused = false;
                        int startIndex = 0;
                        int endIndex = 0;
                        for (int x = startArrayIndex; x < array.Length; x++)
                        {
                            //MessageBox.Show(array[x].ToString());

                            if (array[x] <= volumeThreshold)
                            {
                                if (isPaused == false)
                                {
                                    isPaused = true;
                                    //Start
                                    startIndex = x;
                                }
                                continue;

                            }
                            else
                            {
                                if (isPaused)
                                {
                                    isPaused = false;

                                    endIndex = x;
                                    break;

                                    //pauses.Add(new PauseRange(currentPause.startIndex, currentPause.endIndex));
                                    //currentPause = new PauseRange();
                                }
                            }
                        }

                        //End of File or no more silence
                        if (startIndex == -1 || endIndex == -1)
                            break;

                        int indexLength = (endIndex - startIndex);

                        //Continue to next loop
                        if ((indexLength * sampleLength) <= pauseTimeMin * 1000)
                        {
                            startArrayIndex = endIndex;
                            continue;
                        }

                        //Calculate pause Length
                        int pauseMiddle = startIndex + (indexLength / 2);
                        Timecode splitAt = new Timecode(pauseMiddle * sampleLength);
                        currentTrack = currentTrack.Split(splitAt);
                    }

                    /*
                    Timecode code = new Timecode(array.Length);

                    Timecode splitAt = new Timecode(trackLength.ToMilliseconds() / 2);
                    */
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        class PauseRange
        {
            public int startIndex = 0;
            public int endIndex = 0;

            public PauseRange()
            {
                
            }

            public PauseRange(int start, int end)
            {
                startIndex = start;
                endIndex = end;
            }
        }

        public float[] ReadAudioSamples(string filename)
        {
            using (AudioFileReader reader = new AudioFileReader(filename))
            {
                // Sample Provider ensures 32-bit floating point format
                ISampleProvider isp = reader.ToSampleProvider();

                // Calculate number of samples
                int sampleCount = (int)(reader.Length / (reader.WaveFormat.BitsPerSample / 8));
                float[] buffer = new float[sampleCount];

                // Read samples into buffer
                int samplesRead = isp.Read(buffer, 0, sampleCount);

                return buffer;
            }
        }
        /*
        void AU()
        {
            var reader = new AudioFileReader("audio.mp3");
            Stream extractor = new OffsetSampleProvider(reader)
            {
                SkipOver = TimeSpan.FromSeconds(10),
                Take = TimeSpan.FromSeconds(5)
            };

            float[] buffer = new float[2048];
            int samplesRead = extractor.Read(buffer, 0, buffer.Length);
        }
        */

        TrackEvent[] GetSelectedEvents(Project project)
        {
            List<TrackEvent> selectedList = new List<TrackEvent>();
            foreach (Track track in project.Tracks)
            {
                foreach (TrackEvent trackEvent in track.Events)
                {
                    if(trackEvent.Selected)
                        selectedList.Add(trackEvent);
                }
            }
            return selectedList.ToArray();
        }
        

        Track[] GetSelectedTracks(Project project)
        {
            List<Track> selectedList = new List<Track>();
            foreach (Track track in project.Tracks)
            {
                if(track.Selected)
                    selectedList.Add(track);
            }
            return selectedList.ToArray();
        }

        TrackEvent AddMedia(Project project, string mediaPath, int trackIndex, Timecode start, Timecode length)
        {
            Media media = Media.CreateInstance(project, mediaPath);
            Track track = project.Tracks[trackIndex];

            if (track.MediaType == MediaType.Video)
            {
                VideoTrack videoTrack = (VideoTrack)track;
                VideoEvent videoEvent = videoTrack.AddVideoEvent(start, length);
                Take take = videoEvent.AddTake(media.GetVideoStreamByIndex(0));
                return videoEvent;
            }
            else if (track.MediaType == MediaType.Audio)
            {
                AudioTrack audioTrack = (AudioTrack)track;
                AudioEvent audioEvent = audioTrack.AddAudioEvent(start, length);
                Take take = audioEvent.AddTake(media.GetAudioStreamByIndex(0));
                return audioEvent;
            }

            return null;
        }
    }
}