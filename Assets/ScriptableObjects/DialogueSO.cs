using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueSO", fileName = "new DialogueSO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] string actorName;
    [SerializeField] int id;
    [SerializeField] Sprite avatar;
    [SerializeField][TextArea(2, 3)] string message;
    public string ActorName => actorName;
    public int Id => id;
    public Sprite Avatar => avatar;
    public string Message => message;
}
