using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageNumberCanvas : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField] private float numberUpdateSeconds = 0.5f;
    [SerializeField] private float viewThreshold = 0.5f;
    [SerializeField] private float distanceShrinkage = 1.0f;
    [SerializeField] private float maxDistance = 20.0f;
    [SerializeField] private float defaultFontSize = 40.0f;
    [SerializeField] private float smallestFontSize = 20.0f;
    [SerializeField] private float textLifetime = 3.0f;
    
    private GameObject[] enemies = { };
    private List<DamageNumberText> damageNumbers = new();
    private Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        StartCoroutine(UpdateNumbers());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SummonDamageNumbers();
        }
    }

    IEnumerator UpdateNumbers()
    {
        while (SceneManager.loadedSceneCount > 0)
        {
            yield return new WaitForSeconds(numberUpdateSeconds);

            if (damageNumbers.Count > 0)
            {
                foreach (var num in damageNumbers)
                {
                    if (num.lifeTime < textLifetime)
                    {
                        // Kill if null
                        if (num.enemy == null)
                        {
                            num.lifeTime = textLifetime;
                            continue;
                        }
                        
                        Vector3 screenPos = cam.WorldToScreenPoint(num.enemy.transform.position);
                        Vector3 dest = new Vector3(screenPos.x, screenPos.y + 100.0f, screenPos.z);
                        screenPos = Vector3.Lerp(screenPos, dest, Mathf.Min(num.lifeTime/textLifetime, 1.0f));
                        num.gameObject.GetComponent<RectTransform>().position = screenPos;

                        float distToCam = Vector3.Distance(cam.transform.position, num.enemy.transform.position);
                        Debug.Log(distToCam);
                        if (distToCam < maxDistance)
                        {
                            num.text.enabled = true;
                            // No idea how to calculate this BUT
                            // At dist 0 to player, it should be maxFont
                            // At dist <maxDistance> to player, it should be minFont
                            // Anything beyond that is ignored.
                            num.text.fontSize = (maxDistance / distToCam) * smallestFontSize;
                            
                        }
                        else
                        {
                            num.text.enabled = false;
                            continue;
                        }
                            
                        Vector3 dirToEnemy = num.enemy.transform.position - cam.transform.position;
                        dirToEnemy = dirToEnemy.normalized;
                        float dotProd = Vector3.Dot(dirToEnemy, cam.transform.forward);
                        if (dotProd >= viewThreshold)
                        {
                            num.text.enabled = true;
                        }
                        else
                        {
                            num.text.enabled = false;
                        }
                    }
                }
            }

            // CLEANS UP LIFETIME-EXPIRED DAMAGE NUMBERS FROM SCENE AND LIST
            foreach (var num in damageNumbers)
            {
                if (num.lifeTime >= textLifetime || num.enemy == null)
                {
                    num.gameObject.SetActive(false);
                }
            }
            
            damageNumbers.RemoveAll(x => !x.gameObject.activeInHierarchy);
        }
    }

    public void SummonDamageNumbers()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                GameObject damageNum = ObjectPool.SharedInstance.GetPooledObject();
                if (damageNum != null)
                {
                    damageNumbers.Add(damageNum.GetComponent<DamageNumberText>());
                    damageNum.GetComponent<DamageNumberText>().InstantiateDamageNumber(101, enemy);
                    damageNum.SetActive(true);
                    damageNum.transform.SetParent(transform);
                    Debug.Log("New damage num added!");
                }
            }
        }

    }
}
















