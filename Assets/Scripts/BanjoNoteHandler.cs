using UnityEngine;

public class BanjoNoteHandler : MonoBehaviour
{
    [SerializeField] private AudioSource A;
    [SerializeField] private AudioSource B;
    [SerializeField] private AudioSource E;
    [SerializeField] private AudioSource G;


    public void PlayA()
    {
        A.Play();
    }

    public void PlayB()
    {
        B.Play();
    }

    public void PlayE()
    {
        E.Play();
    }

    public void PlayG()
    {
        G.Play();
    }
}
