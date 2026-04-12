using UnityEngine;

[CreateAssetMenu(fileName = "NouvelAlbum", menuName = "EchoesAR/AlbumData")]
public class AlbumData : ScriptableObject
{
    public string nomAlbum;
    public Sprite photoArtiste; // L'image centrale
    public Sprite ficheAnecdote; // design exporté de Canva
    public Sprite ficheChiffres;  //  design exporté de Canva
    public Sprite ficheHistoire;  // design exporté de Canva
}