using UnityEngine;

public class Data
{
    // 저장 데이터 숫자만 가능
    public int[] Save(int[] data, string name) // 저장할 배열 , 그리고 게임 이름
    {
        int[] s = data;
        for (int i = 0; i < s.Length; i++) PlayerPrefs.SetInt("save" + i + name, s[i]);

        return s;
    }

    public int[] Load(int[] data, string name) // 불러올 데이터 , 저장할떄 쓴 이름
    {
        int[] s = data;
        for (int i = 0; i < s.Length; i++) s[i] = PlayerPrefs.GetInt("save" + i + name);
      
        return s;
    }
}
