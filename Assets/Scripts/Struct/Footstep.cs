using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Footstep
{
    [HideInInspector] public string groundTag;

    [Space]
    [SerializeField] private AudioClip[] movementClips;
    [SerializeField] private AudioClip[] landedClips;


    public Footstep(string groundTag)
    {
        this.groundTag = groundTag;
        movementClips = new AudioClip[0];
        landedClips = new AudioClip[0];
    }
    public bool IsSameGroundTag(string groundTag)
    {
        return groundTag.ToLower() == groundTag.ToLower();
    }

    public AudioClip GetRandomMovementClip()
    {
        return GetRandomClip(movementClips);
    }

    public AudioClip GetRandomLandingClip()
    {
        return GetRandomClip(landedClips);
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips != null) 
        {
            if (clips.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, clips.Length);
                return clips[index];
            }
        }

        return null;
    }

    public static void SetArray(ref Footstep[] footsteps)
    {
        string[] groundTags = Enum.GetNames(typeof(GroundTag));

        if (footsteps != null)
        {
            footsteps = new Footstep[0];
        }

        if(footsteps.Length != groundTags.Length)
        {
            Footstep[] result = new Footstep[groundTags.Length];

            for (int i = 0; i < groundTags.Length; i++)
            {
                bool isSameGroundTag = false;

                for (int j = 0; j < footsteps.Length; i++)
                {
                    if (footsteps[j].IsSameGroundTag(groundTags[i]))
                    {
                        isSameGroundTag = true;
                        result[i] = footsteps[j];
                        break;
                    }
                }

                if (!isSameGroundTag)
                {
                    result[i] = new Footstep(groundTags[i]);
                }
            }

            footsteps = result;
        }
    }

}
