using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TPToRoom : MonoBehaviour
{
    [SerializeField] private GameObject Entry;
    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject TPIn;
    [SerializeField] private GameObject StylizedFPCamera;

    private FPMovement fPMovement;
    private ThirdMovement thirdMovement;
    private CharacterController characterController;
    [SerializeField] private GameObject FPCamera;
    [SerializeField] private GameObject TPCamera;
    void Start()
    {
        fPMovement = Player.GetComponent<FPMovement>();
        thirdMovement = Player.GetComponent<ThirdMovement>();
        characterController = Player.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine("Countdown");
        }
    }
    private IEnumerator Countdown()
    {
        Background.SetActive(true);
        yield return new WaitForSeconds(2);
        TPCamera.SetActive(false);
        FPCamera.SetActive(true);
        StylizedFPCamera.SetActive(true);
        thirdMovement.enabled = false;
        characterController.enabled = true;
        fPMovement.enabled = true;
        yield return new WaitForSeconds(3);
        Player.transform.position = TPIn.transform.position;
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        Background.SetActive(false);
    }
}
