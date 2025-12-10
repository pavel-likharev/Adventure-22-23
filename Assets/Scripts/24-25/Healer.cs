using UnityEngine;

public class Healer : MonoBehaviour
{
    private int _healValue = 20;

    public void Inizialize(int healValue)
    {
        _healValue = healValue;
    }

    public void Heal(Health health) => health.AddValue(_healValue);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerAgentCharacter playerAgentCharacter))
        {
            playerAgentCharacter.Heal(_healValue);
            Destroy(gameObject);
        }
    }

}
