
using System.Windows.Forms;
using System.Collections.Generic;
//using Sony.Vegas;
using ScriptPortal.Vegas;
using NAudio.Wave;
using System;

namespace SuperBunnyCut 
{
    public class EntryPoint
    {
        public float volumeThreshold = 0.01f;

        public float pauseTimeMin = 0.2f;  //1/5th Second
        //public int percentageCut = 50;  //Cut in the middle of the silence
        //public float pauseCutOffLength = 10; //Will auto cut after 10 seconds

        /// <summary>
        /// Total length of read audio file in milliseconds
        /// </summary>
        private static double totalTime = 0;

        public void FromVegas(Vegas vegas)
        {
            //MessageBox.Show(vegas.Version);
            //MessageBox.Show(vegas.Project.Length.ToString());

            try
            {
                SplitAtSilence(vegas);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error:\n" + e, "ERROR");
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

        void SplitAtSilence(Vegas vegas)
        {
            TrackEvent[] selectedEvents = GetSelectedEvents(vegas.Project);
            if (selectedEvents.Length == 0)
                return;

            List<PauseRange> pauses = new List<PauseRange>();

            for (int i = 0; i < selectedEvents.Length; i++)
            {
                AudioEvent track = selectedEvents[i] as AudioEvent;
                float[] mediaSamples = ReadAudioSamples(track.ActiveTake.Media.FilePath);
                double sampleLength = track.Length.ToMilliseconds() / mediaSamples.Length;
                int samplesPerMillisecond = (mediaSamples.Length / (int)totalTime);
                int minPauseSamples = (int)(pauseTimeMin * 1000) * samplesPerMillisecond;

                //MessageBox.Show("Start " + track.Start + " End: " + track.End + " Length: " + track.Length + " SyncOffset: " + track.SyncOffset + " TakeOffset: " + track.ActiveTake.Offset);

                bool isPaused = false;
                int startIndex = 0;
                int endIndex = 0;
                for (int x = 0; x < mediaSamples.Length; x++)
                {
                    //MessageBox.Show(array[x].ToString());

                    if (mediaSamples[x] <= volumeThreshold)
                    {
                        //Get our start position
                        if (isPaused == false)
                        {
                            isPaused = true;
                            startIndex = x;
                        }
                        else if (x == mediaSamples.Length - 1 && startIndex != 0)
                        {
                            //End of Track, force cut
                            endIndex = x;
                            pauses.Add(new PauseRange(startIndex, endIndex));
                            startIndex = 0;
                            endIndex = 0;
                        }
                        continue;
                    }
                    else
                    {
                        if (isPaused)
                        {
                            isPaused = false;
                            //Within Threshold
                            if (x - startIndex >= minPauseSamples)
                            {
                                endIndex = x;
                                pauses.Add(new PauseRange(startIndex, endIndex));
                                startIndex = 0;
                                endIndex = 0;
                                continue;
                            }
                            else
                            {
                                //Reset
                                startIndex = 0;
                                endIndex = 0;
                            }
                        }
                    }
                }

               MessageBox.Show("Found Cuts: " + pauses.Count);

                //Cut in Reverse
                for (int x = pauses.Count - 1; x >= 0; x--)
                {
                    //End of File or no more silence
                    if (pauses[x].startIndex == -1 || pauses[x].endIndex == -1)
                        continue;

                    int indexLength = (pauses[x].endIndex - pauses[x].startIndex);
                    //Calculate pause Length
                    int pauseMiddle = pauses[x].startIndex + (indexLength / 2);

                    Timecode splitAt = new Timecode(pauseMiddle * sampleLength);
                    track.Split(splitAt);
                }
            }

        }

        void Cutter(Vegas vegas)
        {

            TrackEvent[] selectedEvents = GetSelectedEvents(vegas.Project);
            if (selectedEvents.Length == 0)
                return;

            List<PauseRange> pauses = new List<PauseRange>();

            for (int i = 0; i < selectedEvents.Length; i++)
            {
                //MessageBox.Show(selectedEvents[i].MediaType.ToString());
                //MessageBox.Show(selectedEvents[i].ActiveTake.Media.FilePath);

                //Track our current focused Track when we cut it up
                TrackEvent currentTrack = selectedEvents[i];

                int newStartIndex = 0;
                AudioEvent track = currentTrack as AudioEvent;
                float[] mediaSamples = ReadAudioSamples(track.ActiveTake.Media.FilePath);

                //Timing
                double millisecondsPerSample = (totalTime / mediaSamples.Length);
                int samplesPerMillisecond = (mediaSamples.Length / (int)totalTime);
                int minPauseSamples = (int)(pauseTimeMin * 1000) * samplesPerMillisecond;


                while (track != null)
                {
                    track = currentTrack as AudioEvent;
                    if (track == null)
                        break;

                    // The time the Event Starts
                    // track.Start

                    // The Time the Event Ends
                    // track.End

                    // The current cut length of the event
                    // track.Length

                    // How much time was cut off the FRONT of this track's event
                    // track.ActiveTake.Offset

                    // How long the Audio File is
                    // totalTime (Read after Read Audio Samples), rounds down to millisecond


                    //MessageBox.Show("Lowest: " + lowest * 100 + " Highest: " + highest * 100);


                    //MessageBox.Show("Start " + track.Start + " End: " + track.End + " Length: " + track.Length + " TakeOffset: " + track.ActiveTake.Offset + " samples: " + mediaSamples.Length
                    //    + "\n Milli Per Sample: " + millisecondsPerSample
                    //    + "\n Sample per Milli: " + samplesPerToMillisecond);

                    //Start Index - Use the prev end index as it skips over the silence (in Theory)
                    int mediaStartIndex = newStartIndex;
                    if (mediaStartIndex == 0)
                        mediaStartIndex = samplesPerMillisecond * (int)track.ActiveTake.Offset.ToMilliseconds();
                    //double startTimeOffset = totalTime - track.ActiveTake.Offset.ToMilliseconds();

                    //MessageBox.Show("Track is " + track.Length.ToMilliseconds() + " Milliseconds long and array: " + mediaSamples.Length + " Total Time: " + totalTime);

                    //MessageBox.Show("Track is " + track.Length.ToMilliseconds() + " Milliseconds long and array: " + mediaSamples.Length);

                    //double sampleLength = track.Length.ToMilliseconds() / mediaSamples.Length;

                    bool isPaused = false;
                    int startIndex = -1;
                    int endIndex = -1;
                    for (int x = mediaStartIndex; x < mediaSamples.Length; x++)
                    {
                        if (mediaSamples[x] < volumeThreshold)
                        {
                            //Get our start position
                            if (isPaused == false)
                            {
                                isPaused = true;
                                startIndex = x;
                            }
                            else if (x == mediaSamples.Length - 1 && startIndex != -1)
                            {
                                endIndex = x;
                            }
                            continue;
                        }
                        else
                        {
                            if (isPaused)
                            {
                                isPaused = false;
                                //Within Threshold
                                if (x - startIndex >= minPauseSamples)
                                {
                                    endIndex = x;
                                    break;
                                }
                                //Else we try again
                            }
                        }
                    }

                    MessageBox.Show("Silence from: " + new Timecode(startIndex * millisecondsPerSample) + " to: " + new Timecode(endIndex * millisecondsPerSample));

                    //End of File or no more silence
                    if (startIndex == -1 || endIndex == -1)
                        break;

                    int indexLength = (endIndex - startIndex);

                    //newStartIndex = endIndex;

                    //Calculate pause Length
                    int pauseMiddleIndex = startIndex + (indexLength / 2);


                    //TEST
                    //TrackEvent prev = currentTrack;
                    //currentTrack.Split(new Timecode(endIndex * millisecondsPerSample));
                    //prev.Split(new Timecode(startIndex * millisecondsPerSample));


                    Timecode splitAt = new Timecode(pauseMiddleIndex * millisecondsPerSample);
                    currentTrack = currentTrack.Split(splitAt);
                }

                /*
                Timecode code = new Timecode(array.Length);

                Timecode splitAt = new Timecode(track.Length.ToMilliseconds() / 2);
                */
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

                // Pass to class for reading later
                totalTime = reader.TotalTime.TotalMilliseconds;

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