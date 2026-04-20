using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlbumAnimationSequence : MonoBehaviour
{
    [Header("Réglages")]
    public float vitesseAnimation = 1.5f;
    public float hauteurPop = 0.05f;

    [Header("Ordre des Quads")]
    public Renderer photoArtiste;
    public List<Renderer> autresQuads;

    private void OnEnable()
    {
        // On s'assure que tout est éteint au départ
        ResetRenderers();
        // On lance la séquence ordonnée
        StartCoroutine(PlayFullSequence());
    }

    void ResetRenderers()
    {
        if (photoArtiste) photoArtiste.enabled = false;
        foreach (var r in autresQuads) if (r) r.enabled = false;
    }

    IEnumerator PlayFullSequence()
    {
        // D'abord l'artiste
        if (photoArtiste)
        {
            yield return StartCoroutine(FadeAndPop(photoArtiste, photoArtiste.transform.localPosition));
        }

        // Pause de 1.5s
        yield return new WaitForSeconds(1.5f);

        //Puis les autres un par un avec 1s d'écart
        foreach (var r in autresQuads)
        {
            if (r)
            {
                yield return StartCoroutine(FadeAndPop(r, r.transform.localPosition));
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    // fonction logique de mouvement et fondu
    IEnumerator FadeAndPop(Renderer rend, Vector3 targetPos)
    {
        rend.enabled = true;
        float alpha = 0;
        Vector3 startPos = targetPos - new Vector3(0, hauteurPop, 0);

        while (alpha < 1)
        {
            alpha += Time.deltaTime * vitesseAnimation;

            // Appliquer la transparence au matériau
            Color c = rend.material.color;
            c.a = alpha;
            rend.material.color = c;

            // Appliquer le mouvement
            rend.transform.localPosition = Vector3.Lerp(startPos, targetPos, alpha);
            yield return null;
        }

        // on force les valeurs finales
        Color finalColor = rend.material.color;
        finalColor.a = 1;
        rend.material.color = finalColor;
        rend.transform.localPosition = targetPos;
    }
}