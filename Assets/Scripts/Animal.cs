using UnityEngine;

public class Animal : MonoBehaviour
{
    private Animator animator;
    public enum State { Idle, Walk, Sleep, Sick }
    private State currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        ChangeState(State.Idle);
    }

    void Update()
    {
        // Hayvan durum deðiþiklikleri buraya
    }

    public void ChangeState(State newState)
    {
        currentState = newState;
        animator.SetTrigger(newState.ToString());
    }
}
