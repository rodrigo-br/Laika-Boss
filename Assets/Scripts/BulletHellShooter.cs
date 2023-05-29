using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int burstCount = 1;
    [SerializeField] int projectilesPerBurst = 36;
    [SerializeField][Range(0, 359)] float angleSpread = 359f;
    [SerializeField] float startingDistance = 0.1f;
    [SerializeField] float timeBetweenShoots = 0.1f;
    [SerializeField] float timeBetweenBursts = 1f;

    public void InstantiateBullets(Vector2 initialTarget)
    {
        initialTarget *= -1;
        StartCoroutine(ShootingRoutine(initialTarget));
    }

    IEnumerator ShootingRoutine(Vector2 target)
    {
        float targetAngle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        float startAngle = targetAngle;
        float endAngle = targetAngle;
        float currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        float angleStep = 0f;

        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }

        for (int i = 0; i < burstCount; i++)
        {
            for (int j = 0; j < projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.up = newBullet.transform.position - transform.position;
                currentAngle += angleStep;
                yield return new WaitForSeconds(timeBetweenShoots);
            }
            currentAngle = startAngle;
            yield return new WaitForSeconds(timeBetweenBursts);
        }
    }

    Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
