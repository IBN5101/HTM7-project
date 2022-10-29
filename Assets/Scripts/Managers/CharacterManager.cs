using UnityEngine;

public class CharacterManager : StaticInstance<CharacterManager> {

    [SerializeField] private Player _player;
    public Player Player => _player;

    public void SpawnHeroes() {
        SpawnCharacter(_player, new Vector3(3f, 3f, 3f));
    }

    void SpawnCharacter(Player player, Vector3 pos) {
        player = _player;

        Instantiate(_player, pos, Quaternion.identity, transform);
    }
}