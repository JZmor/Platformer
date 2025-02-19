using UnityEngine;

public class QuestionMove : MonoBehaviour
{
    public float questionOffset = 0f;
    public int changeFrame = 80;
    public int changeFrame2 = 240;
    
    private MeshRenderer meshRenderer;
    public int offFrame = 0;
    public int offFrame2 = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (offFrame2 == changeFrame2)
        {
            if (questionOffset >= 1f)
            {
                offFrame2 = 0;
                questionOffset = 0;
            }
            if (offFrame == changeFrame)
            {
                offFrame = 0;
                questionOffset += 0.2f;
                meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, questionOffset));
            }
            else
            {
                offFrame++;
            }
        }
        else
        {
            offFrame2++;
        }
    }
}
