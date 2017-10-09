namespace HK.STG.InputSystems
{
    public sealed class InputData
    {
        public InputType Type { private set; get; }

        public void Set(InputType type)
        {
            this.Type = type;
        }
    }
}
