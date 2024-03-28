namespace HellRework.Globals.GItems
{
    public class ReworkItem : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item Item)
        {
            switch (Item.type)
            {
                case ItemID.Flamarang:
                    Item.damage = 26;
                    Item.useTime = 17;
                    Item.useAnimation = 17;
                    Item.shootSpeed *= 1.25f;
                    break;
                default:
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            //Check if a replacement tooltip is present. If so, replace the existing tooltip with a new one.
            string newTooltip = null;

            if (Language.Exists("Mods.HellRework.NewItemDescriptions." + item.Name.Replace(" ", "")))
            {
                newTooltip = Language.GetTextValue("Mods.HellRework.NewItemDescriptions." + item.Name.Replace(" ", ""));
            }

            if (newTooltip != null)
            {
                tooltips.RemoveAll(x => x.Mod == "Terraria" && x.Name.Contains("Tooltip"));

                int index = tooltips.FindIndex(0, x => x.IsModifier);
                if (index == -1) index = tooltips.Count;

                tooltips.Insert(index, new TooltipLine(Mod, "ReworkedTooltip", newTooltip));
            }
        }
    }
}
