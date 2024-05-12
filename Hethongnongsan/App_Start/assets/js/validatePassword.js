function validatePassword() {
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;
    var errorDiv = document.getElementById("passwordError");

    // Kiểm tra độ dài của mật khẩu
    if (password.length < 4) {
        errorDiv.textContent = "Password must be at least 4 characters long.";
        return false;
    }

    // Kiểm tra mật khẩu và xác nhận mật khẩu có khớp nhau không
    if (password !== confirmPassword) {
        errorDiv.textContent = "Passwords do not match.";
        return false;
    }

    // Nếu mọi thứ đều hợp lệ, submit form
    errorDiv.textContent = "";
    alert("Form submitted successfully!");
    return true;
}