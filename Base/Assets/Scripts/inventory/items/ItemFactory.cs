using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory {

    public static Item fromTag(string tag)
    {
        switch(tag)
        {
            case "05lb":
                return new Block05();
            case "1lb":
                return new Block1();
            case "2lb":
                return new Block2();
            case "3lb":
                return new Block3();
            case "5lb":
                return new Block5();
        }
        return new Block05();
    }
}
