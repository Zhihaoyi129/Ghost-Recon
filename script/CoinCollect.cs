using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioClip collectSound; // 收集音效
    public int coinValue = 1; // 金币价值（可选，用于计分）
    
    private AudioSource audioSource;
    
    void Start()
    {
        // 获取或添加AudioSource组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 检查碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 播放音效
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            
            
            // 禁用金币对象（使其不可见且不互动）
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            // 音效播放完后销毁对象
            Destroy(gameObject, collectSound != null ? collectSound.length : 0.1f);
        }
    }
}