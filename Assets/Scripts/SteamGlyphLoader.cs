using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SteamGlyphLoader : MonoBehaviour
{

    // Erstat med Xinput-pladsen, som du foresp�rger. Dette er nummer mellem 0 og 3
    // Hvis du bruger RawInput til at s�ge efter enheder, f�r du beslutter, hvilken API du vil bruge, s� skal du
    // se sektionen "Brug af RawInput til enhedssporing".
    // Start is called before the first frame update
    private static int nXinputSlot = 2;


    public static void LoadSteamGlyph(EInputActionOrigin glyph, RawImage image)
    {
        // Erstat med knappen, du foresp�rger
        //EXboxOrigin eXboxButtonToGetGlyphFor = EXboxOrigin.k_EXboxOrigin_A;
        EInputActionOrigin buttonOrigin = glyph;

        // Hvis controlleren er konfigureret gennem Steam Input � overs�t knappen
        InputHandle_t controller1Handle = SteamInput.GetControllerForGamepadIndex(nXinputSlot);
        if (controller1Handle.m_InputHandle > 0)
        {
            // Gyldige handles er non-zero, dette er en controller konfigureret gennem Steam Input
            // Bem�rk: Controllere, som bruger Steam Input-API returnerer ikke et handle gennem GetControllerForGamepadIndex()
            buttonOrigin = SteamInput.GetActionOriginFromXboxOrigin(controller1Handle, EXboxOrigin.k_EXboxOrigin_A);
        }
        else
        {
            // Gyldige handles er non-zero, dette er en normal Xbox-controller
            // Forts�t med at bruge den originale knap
        }
        // EInputActionOrigin-v�rdierne forts�tter med at vokse, idet Steam tilf�jer underst�ttelse, men dette er OK, fordi
        // i dette eksempel f�r vi enhedens billeder fra Steam, som ogs� giver et nyt ikonbillede

        // F� billedet fra Steam-klienten
        string localGlyphPath = SteamInput.GetGlyphPNGForActionOrigin(buttonOrigin, ESteamInputGlyphSize.k_ESteamInputGlyphSize_Medium, 0);
        
        // "path = C:\Programmer (x86)\Steam\tenfoot\resource\images\library\controller\api\ps4_button_x.png"
        // Erstat dette med en funktion fra spillet, som laver en filsti til en brugbar spiltekstur
        image.texture = LoadPNG(localGlyphPath);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
