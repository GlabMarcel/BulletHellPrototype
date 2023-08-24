using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject noteProjectilePrefab; // Drag the NoteProjectile prefab here in the inspector
    public float projectileSpeed = 30f;
    public Vector2 offset = new Vector2(0.5f, 0); // Offset to spawn the projectile from the player's center

    public AudioSource audioSource; // Drag the AudioSource component here in the inspector
    public AudioClip[] musicNotes; // Drag your music note audio clips here in the inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Shoot();
            PlayMusicNote();
        }
    }

    void Shoot()
    {
        Vector2 shootingDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Vector2 spawnPosition = (Vector2)transform.position + offset * shootingDirection.magnitude;
        
        GameObject note = Instantiate(noteProjectilePrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = note.GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * projectileSpeed;
        NoteProjectile noteProjectile = note.GetComponent<NoteProjectile>();
        noteProjectile.damage = 1; 
    }

    void PlayMusicNote()
    {
        int randomNoteIndex = Random.Range(0, musicNotes.Length); // Choose a random note to play
        audioSource.clip = musicNotes[randomNoteIndex];
        audioSource.Play();
    }
}
