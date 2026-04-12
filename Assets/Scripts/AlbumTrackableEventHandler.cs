using UnityEngine;

public class AlbumTrackableEventHandler : MonoBehaviour
{
    public MeshRenderer photoRenderer;
    public MeshRenderer anecdoteRenderer;
    public MeshRenderer chiffresRenderer;
    public MeshRenderer HistoireRenderer;

    public void SetupAlbum(AlbumData data)
    {
        photoRenderer.material.mainTexture = data.photoArtiste.texture;
        anecdoteRenderer.material.mainTexture = data.ficheAnecdote.texture;
        chiffresRenderer.material.mainTexture = data.ficheChiffres.texture;
        HistoireRenderer.material.mainTexture = data.ficheHistoire.texture;
    }
}