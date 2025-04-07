using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TPOuttaRoom : MonoBehaviour
{
    [SerializeField] private GameObject Entry;
    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject TPOut;
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
        FPCamera.SetActive(false);
        StylizedFPCamera.SetActive(false);
        TPCamera.SetActive(true);
        fPMovement.enabled = false;
        characterController.enabled = false;
        thirdMovement.enabled = true;
        yield return new WaitForSeconds(3);
        Player.transform.position = TPOut.transform.position;
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        Background.SetActive(false);
    }
}
