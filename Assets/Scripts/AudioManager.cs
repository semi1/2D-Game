using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Playerdeath;
    public AudioSource Enemydeath;
    public AudioSource Coins;
    public AudioSource Hurt;
    public AudioSource Run;

    public void playPlayerDeath()
    {
        Playerdeath.Play();
    }
    public void playEnemyDeath()
    {
        Enemydeath.Play();
    }
    public void playCoins()
    {
        Coins.Play();
    }
    public void playHurt()
    {
        Hurt.Play();
    }public void playRun()
    {
        Run.Play();
    }
}
