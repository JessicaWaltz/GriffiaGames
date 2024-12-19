using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthController : MonoBehaviour
{
    public int startingHealth = 5; // Set your starting health here
    public Text lives;
    private int currentHealth;
    private Text healthText;
    private FadeToBlackController fadeController; // Reference to your FadeToBlackController
    private GameObject player;

    void Start()
    {
        healthText = GetComponent<Text>(); // Automatically get the Text component on the GameObject
        currentHealth = startingHealth;
        UpdateHealthText();

        // Assuming FadeToBlackController is attached to the same GameObject, otherwise, find it appropriately
        fadeController = FindObjectOfType<FadeToBlackController>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Call this method when the player is hit
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHealthText();
        }

        if (currentHealth == 0)
        {
            // Player has no more health, wait for 3 seconds and then perform game over actions
            StartCoroutine(FadeBlackoutSquareWithDelay(3f));
        }
    }

    IEnumerator FadeBlackoutSquareWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        fadeController.StartCoroutine(fadeController.FadeBlackoutSquare());
        yield return new WaitForSeconds(1);
        int currentLives = int.Parse(lives.text);
        if (currentLives >= 1)
        {
            currentLives--;
            // Set health back to max
            currentHealth = startingHealth;
            UpdateHealthText();
            // Update the lives text
            UpdateLivesText(currentLives);

            // Delete all enemies (Replace "Enemy" with the tag of your enemy objects)
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            GameObject[] triggers = GameObject.FindGameObjectsWithTag("trigger");
            foreach (GameObject trigger in triggers)
            {
                spawnEnemies spawnEnemiesComponent = trigger.GetComponent<spawnEnemies>();
                if (spawnEnemiesComponent != null)
                {
                    spawnEnemiesComponent.ResetSpawner();
                }
                else
                {
                    Debug.LogWarning("spawnEnemies component not found on " + trigger.name);
                }
            }
            player.transform.position = player.GetComponent<StarPlayerController>().respawnLocation;
            yield return new WaitForSeconds(delay);
            fadeController.StartCoroutine(fadeController.FadeBlackoutSquare(false));
            // Spawn the player at the last spawn point
            //transform.position = lastSpawnPoint;


        }
        else { 
        //display game over
        }
        
        

        

    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    void UpdateHealthText()
    {
        // Update the UI text with dots representing health
        healthText.text = new string('.', currentHealth);
    }
    void UpdateLivesText(int currentLives)
    {
        // Update the UI text with the current number of lives
        lives.text = currentLives.ToString();
    }
}
