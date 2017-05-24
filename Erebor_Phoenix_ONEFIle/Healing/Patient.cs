using Phoenix.WorldData;

namespace Phoenix.EreborPlugin.Healing
{
    public class Patient
    {
        public UOCharacter character { get; set; }
        public int equip { get; set; }
        public Patient(UOCharacter ch, int eq)
        {
            this.character = ch;
            this.equip = eq;
        }
        public Patient(string createString)
        {
            string[] tm = createString.Split(';');
            character = new UOCharacter(uint.Parse(tm[0].Substring(2), System.Globalization.NumberStyles.HexNumber));
            equip = int.Parse(tm[1]);

        }
        public override string ToString()
        {
            return character.Serial.ToString() + ";" + equip.ToString();
        }

    }
}
