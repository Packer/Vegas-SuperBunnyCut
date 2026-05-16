using System;
using System.Collections.Generic;
using ScriptPortal.Vegas;
using NAudio.Wave;

namespace SuperBunnyCut
{
    public class Global
    {
        public static double totalTime;

        public static float[] ReadAudioSamples(string filename)
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

        public static TrackEvent[] GetSelectedEvents(Project project)
        {
            List<TrackEvent> selectedList = new List<TrackEvent>();
            foreach (Track track in project.Tracks)
            {
                foreach (TrackEvent trackEvent in track.Events)
                {
                    if (trackEvent.Selected)
                        selectedList.Add(trackEvent);
                }
            }
            return selectedList.ToArray();
        }


        public static Track[] GetSelectedTracks(Project project)
        {
            List<Track> selectedList = new List<Track>();
            foreach (Track track in project.Tracks)
            {
                if (track.Selected)
                    selectedList.Add(track);
            }
            return selectedList.ToArray();
        }
    }

    public class PauseRange
    {
        public int startIndex = 0;
        public int endIndex = 0;

        public float startMilliseconds = 0;
        public float endMilliseconds = 0;

        public PauseRange()
        {

        }

        public PauseRange(int start, int end, float startMilli, float endMilli)
        {
            startIndex = start;
            endIndex = end;
            startMilliseconds = startMilli;
            endMilliseconds = endMilli;
        }
    }
}
