using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;

    // Person that shot
    public string owner;
    public Photon.Pun.PhotonView view;

    private void Start()
    {
        view = FindObjectOfType<Photon.Pun.PhotonView>();
    }

    /*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
    void Update () {

		if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer)){
			if(decalHitWall){
				if(hit.transform.tag == "LevelPart"){
					Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}

                if (hit.transform.tag == "Target")
                {
                    GameObject decalWall = Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal), hit.transform);
                    decalWall.transform.localScale = new Vector3(0.15f, 0.15f, 0.001f);
                }

                if (hit.transform.tag == "Dummie")
                {
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    hit.transform.GetComponent<dummieMovement>().takeDamage(10);
                }
                
                if (hit.transform.tag == "Player"){
                // Blood effecet
				Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

                // Person that got shot 
                Photon.Pun.PhotonView playerPV = hit.transform.gameObject.GetComponent<Photon.Pun.PhotonView>();
                    
                playerPV.RPC("takeDamage", Photon.Pun.RpcTarget.All, 25);

                if(playerPV.GetComponent<PlayerHealth>().playerHealth == 0)
                {
                    playerScoreTracker[] shooterName = FindObjectsOfType<playerScoreTracker>();
                    foreach(playerScoreTracker shooter in shooterName)
                    {
                        if (shooter.playerName == owner)
                        {
                            // Add to kill count
                            shooter.view.RPC("incKills", Photon.Pun.RpcTarget.All);

                            // Show in kill feed
                            FindObjectOfType<killFeed>().view.RPC("addToKillFeed", Photon.Pun.RpcTarget.All, owner, playerPV.GetComponent<playerScoreTracker>().playerName);       
                        }
                    }
                }
                    
				}
			}		
			Destroy(gameObject);
		}
		Destroy(gameObject, 0.1f);
	}

    

}
