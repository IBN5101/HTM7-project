using UnityEngine;

/// <summary>
/// An example of a scene-specific manager grabbing resources from the resource system
/// Scene-specific managers are things like grid managers, unit managers, environment managers etc
/// </summary>
public class CharacterManager : StaticInstance<CharacterManager> {

    [SerializeField] private Player _player;
    public Player Player => _player;

    public void SpawnHeroes() {
        SpawnCharacter(_player, new Vector3(3f, 3f, 3f));
    }

    void SpawnCharacter(Player t, Vector3 pos) {
        var player = _player;

        var spawned = Instantiate(_player, pos, Quaternion.identity, transform);
    }
}