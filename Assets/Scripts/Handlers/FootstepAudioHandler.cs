using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FootstepAudioHandler
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Footstep[] footsteps;

    [Space]
    [SerializeField] private float movementWalkingPitch;
    [SerializeField] private float movementRunningPitch;

    [Space]
    [SerializeField] private float movementPitchTransitionSpeed;

    private float currentMovementPitch;

    public void Validate()
    {
        Footstep.SetArray(ref footsteps);
    }

    public void PlayMovementSound(string groundTag, bool isRunning)
    {
        float desiredMovementPitch = movementWalkingPitch;

        if (!source.isPlaying)
        {
            if (isRunning)
            {
                desiredMovementPitch = movementRunningPitch;
            }

            currentMovementPitch = Mathf.Lerp(currentMovementPitch, desiredMovementPitch, movementPitchTransitionSpeed * Time.deltaTime);
            PlaySound(GetMovementClip(groundTag));
        }
    }

    public void PlayLandingSound(string groundTag)
    {
        currentMovementPitch = movementWalkingPitch;
        PlaySound(GetLandingClip(groundTag));
    }

    private void PlaySound(AudioClip clip)
    {
        source.pitch = currentMovementPitch;
        source.clip = clip;
        source.Play();
    }

    private Footstep GetFootstepByGroundTag(string groundTag) 
    {
        for (int i = 0; i < footsteps.Length; i++)
        {
            if (footsteps[i].IsSameGroundTag(groundTag))
            {
                return footsteps[i];
            }
        }

        return new Footstep();
    }

    private AudioClip GetMovementClip(string groundTag)
    {
        return GetFootstepByGroundTag(groundTag).GetRandomMovementClip();

    }

    private AudioClip GetLandingClip(string groundTag)
    {
        return GetFootstepByGroundTag(groundTag).GetRandomLandingClip();
    }
}
