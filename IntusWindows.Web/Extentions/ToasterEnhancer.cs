using MatBlazor;

namespace IntusWindows.Web.Extentions
{
    public static class ToasterEnhancer
    {
        public static void DataNotFound(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Sorry, {dataType} unavailable.",
                title: "DATA NOT FOUND",
                type: MatToastType.Danger);
        }

        public static void CreateSuccessful(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Your {dataType} has been created successfully.",
                title: "CREATE SUCCESSFUL",
                type: MatToastType.Success);
        }
        public static void AddedSuccessful(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Your {dataType} has been added successfully.",
                title: "ADD SUCCESSFUL",
                type: MatToastType.Success);
        }
        public static void CreateFailed(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Failed to create {dataType}. Try again later.",
                title: "CREATE FAILED",
                type: MatToastType.Danger);
        }
        public static void AddFailed(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Failed to add {dataType}. Try again later.",
                title: "ADD FAILED",
                type: MatToastType.Danger);
        }

        public static void CustomMessage(this IMatToaster toaster, string message)
        {
            toaster.Add(
                message: $"{message}. Try again later.",
                title: "CREATE FAILED",
                type: MatToastType.Danger);
        }

        public static void UpdateSuccessful(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Your {dataType} has been updated successfully.",
                title: "UPDATE SUCCESSFUL",
                type: MatToastType.Success);
        }

        public static void UpdateFailed(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Failed to update {dataType}. Try again later.",
                title: "UPDATE FAILED",
                type: MatToastType.Danger);
        }

        public static void DeleteSuccessful(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Your {dataType} has been deleted successfully.",
                title: "DELETE SUCCESSFUL",
                type: MatToastType.Success);
        }

        public static void DeleteFailed(this IMatToaster toaster, string dataType)
        {
            toaster.Add(
                message: $"Failed to delete {dataType}. Try again later.",
                title: "DELETE FAILED",
                type: MatToastType.Danger);
        }

        public static void BadRequest(this IMatToaster toaster, string message)
        {
            toaster.Add(
                message: message,
                title: "BAD REQUEST",
                type: MatToastType.Danger);
        }
    }
}
