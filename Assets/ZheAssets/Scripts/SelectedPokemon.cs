using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPokemon : SingletonMono<SelectedPokemon>
{
	public string playerAPokemon;
	public string playerBPokemon;
	
    protected override void Awake()
    {
        base.Awake(); 
        DontDestroyOnLoad(this);
    }
}
