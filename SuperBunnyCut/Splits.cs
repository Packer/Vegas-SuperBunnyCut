
using System.Windows.Forms;
using Sony.Vegas;

namespace SuperBunnyCut
{

    public class EntryPoint
    {
        public void FromVegas(Vegas vegas)
        {
            MessageBox.Show(vegas.Version);
            MessageBox.Show(vegas.Project.Length.ToString());

            try
            {
                TrackEvent[] selectedEvents = GetSelectedEvents(vegas.Project);

                if (selectedEvents[0].IsAudio())
                {
                }
                Media media;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
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