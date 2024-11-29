using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "HelloMyNameIsInputValidator", menuName = "Input Field Validator (HelloMyNameIs)")]
public class CustomValidator : TMPro.TMP_InputValidator
{
    public override char Validate(ref string text, ref int pos, char ch)
    {
	    if (text.Length >= 11) return '\0';
	    if (ch == '<') return '\0';
	    if (ch == '>') return '\0'; 
	    text = text.Insert(pos, ch.ToString());
	    pos++;
	    return ch;
    }
}
