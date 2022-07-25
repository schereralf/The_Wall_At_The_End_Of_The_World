using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public Player player;
    public Camera mainCamera;
    public LayerMask groundLayerMask;
    public List<AudioClip> gravelSteps = new List<AudioClip>();
    public List<AudioClip> woodSteps = new List<AudioClip>();
    public List<AudioClip> stoneSteps = new List<AudioClip>();
    public List<AudioClip> snowSteps = new List<AudioClip>();

    public AudioSource source;
    public float baseStepSpeed = 0.35f;
    private float footstepTimer;

    private void Start()
    {
        footstepTimer = baseStepSpeed;
    }
    public void Update()
    {
        if (!player.isGrounded) {return; }
        
        if (player.move == Vector3.zero) {return; }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if(Physics.Raycast(mainCamera.transform.position, Vector3.down, out RaycastHit hit,3,groundLayerMask))
            {
                switch (hit.collider.tag)
                {
                    case "Ground":
                        {
                            source.PlayOneShot(snowSteps[(Random.Range(0, snowSteps.Count))]);
                            break;
                        }
                    case "Wood":
                        {
                            source.PlayOneShot(woodSteps[(Random.Range(0, woodSteps.Count))]);
                            break;
                        }
                    case "Wall":
                        {
                            source.PlayOneShot(stoneSteps[(Random.Range(0, stoneSteps.Count))]);
                            break;
                        }
                    default:
                        {
                            source.PlayOneShot(gravelSteps[(Random.Range(0, gravelSteps.Count))]);
                            break;
                        }
                }
            }
            footstepTimer = baseStepSpeed;
        }
    }
}
