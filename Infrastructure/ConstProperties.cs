namespace Infrastructure
{
    public static class PermissionFlags
    {
        public const string AddProduct = "AddProduct";
        public const string GetProduct = "GetProduct";
        public const string GetAllProducts = "GetAllProducts";
        public const string EditProduct = "EditProduct";
        public const string DeleteProduct = "DeleteProduct";
    }

    public static class ResponseMessage
    {
        public const string Success = "موفق";
        public const string NotFound = "یافت نشد.";
        public const string AccountCreated = "حساب کاربری با موفقیت ایجاد شد.";
        public const string WrongUsernameOrPassword = "نام کاربری یا کلمه عبور اشتباه است.";
        public const string DuplicateUsername = "نام کاربری قابل قبول نمی باشد، نام کاربری دیگری انتخاب کنید.";
    }
}
