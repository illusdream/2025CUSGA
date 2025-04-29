using ilsFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

public class AudioEmitterPlayableBehaviour: PlayableBehaviour
{
    public SoundData soundData;
    
    public string OutputAudioChannel;
    
    private AudioEmitter emitter;

#if UNITY_EDITOR
    AudioSource audioSource;
#endif
    
    public override void OnPlayableCreate(Playable playable)
    {
        audioSource = EditorUtility.CreateGameObjectWithHideFlags("AudioSource" , HideFlags.HideAndDontSave , typeof(AudioSource)).GetComponent<AudioSource>();
        base.OnPlayableCreate(playable);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            audioSource.clip = soundData.clip;
            audioSource.volume = soundData.volume;
            audioSource.Play();
            return;
        }
#endif


        emitter = AudioManager.Instance.Play(OutputAudioChannel,soundData);
        base.OnBehaviourPlay(playable, info);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            audioSource?.Stop();
        }
#endif
        if (playable.GetGraph().IsPlaying())
        {
            emitter?.Stop();
        }
        base.OnBehaviourPause(playable, info);
    }
}