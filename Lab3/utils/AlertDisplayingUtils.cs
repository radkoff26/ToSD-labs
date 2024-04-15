namespace Lab3.utils
{
    internal class AlertDisplayingUtils
    {

        public static void ShowAlertDueToLackOfDataForDrawing()
        {
            MessageBox.Show(
                "Рисование невозможно из-за недостатка данных!",
                "Предупреждение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly
            );
        }
    }
}
