// Product
public class RPGCharacter
{
    public string Hair { get; set; }
    public string Armor { get; set; }
    public string Weapon { get; set; }
    
    public void Show()
    {
        Console.WriteLine($"Hair: {Hair}, Armor: {Armor}, Weapon: {Weapon}");
    }
}

// Builder
public abstract class CharacterBuilder
{
    protected RPGCharacter character;
    
    public void CreateCharacter()
    {
        character = new RPGCharacter();
    }
    
    public RPGCharacter GetCharacter()
    {
        return character;
    }

    public abstract void SetHair();
    public abstract void SetArmor();
    public abstract void SetWeapon();
}

// ConcreteBuilder
public class WarriorBuilder : CharacterBuilder
{
    public override void SetHair() => character.Hair = "Short";
    public override void SetArmor() => character.Armor = "Plate Armor";
    public override void SetWeapon() => character.Weapon = "Sword";
}

public class MageBuilder : CharacterBuilder
{
    public override void SetHair() => character.Hair = "Long";
    public override void SetArmor() => character.Armor = "Cloth Robe";
    public override void SetWeapon() => character.Weapon = "Staff";
}

// Director
public class CharacterCreator
{
    private CharacterBuilder builder;

    public CharacterCreator(CharacterBuilder builder)
    {
        this.builder = builder;
    }

    public void CreateCharacter()
    {
        builder.CreateCharacter();
        builder.SetHair();
        builder.SetArmor();
        builder.SetWeapon();
    }

    public RPGCharacter GetCharacter()
    {
        return builder.GetCharacter();
    }
}

// Usage
class Program
{
    static void Main()
    {
        var warriorBuilder = new WarriorBuilder();
        var mageBuilder = new MageBuilder();

        var creator = new CharacterCreator(warriorBuilder);
        creator.CreateCharacter();
        var warrior = creator.GetCharacter();
        warrior.Show();

        creator = new CharacterCreator(mageBuilder);
        creator.CreateCharacter();
        var mage = creator.GetCharacter();
        mage.Show();
    }
}
