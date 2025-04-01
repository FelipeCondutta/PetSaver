document.addEventListener("DOMContentLoaded", () => {
    const container = document.querySelector(".container");
    const registerBtn = document.querySelector(".register-btn");
    const loginBtn = document.querySelector(".login-btn");

    registerBtn.addEventListener("click", () => {
        container.classList.add("active");
    });

    loginBtn.addEventListener("click", () => {
        container.classList.remove("active");
    });



    // Verificar se o usuário já está autenticado
    const token = localStorage.getItem("token");
    const loginButton = document.querySelector("#loginBtn");
    const accountButton = document.querySelector("#accountBtn");

    if (token) {
        if (loginButton) loginButton.style.display = "none";
        if (accountButton) accountButton.style.display = "block";
    } else {
        if (loginButton) loginButton.style.display = "block";
        if (accountButton) accountButton.style.display = "none";
    }

    // Registro de Usuário
    document.querySelector(".register form")?.addEventListener("submit", async (e) => {
        e.preventDefault();

        const name = document.querySelector(".register input[name='username']").value;
        const email = document.querySelector(".register input[name='email']").value;
        const password = document.querySelector(".register input[name='password']").value;

        try {
            const response = await fetch("/Account/Register", {  // 🔹 Caminho ajustado para o ASP.NET MVC
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ name, email, password }),
            });

            const data = await response.json();

            if (response.ok) {
                container.classList.remove("active");
                alert("Cadastro realizado com sucesso!");
            } else {
                alert(data.message || "Erro ao cadastrar usuário.");
            }
        } catch (error) {
            console.error("Erro ao cadastrar:", error);
            alert("Erro ao cadastrar usuário.");
        }
    });

    // Login de Usuário
    document.querySelector(".login form")?.addEventListener("submit", async (e) => {
        e.preventDefault();

        const email = document.querySelector(".login input[name='email']").value;
        const password = document.querySelector(".login input[name='password']").value;

        try {
            const response = await fetch("/Account/Login", {  // 🔹 Caminho ajustado para ASP.NET MVC
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
            });

            const data = await response.json();

            if (response.ok) {
                localStorage.setItem("token", data.token);
                localStorage.setItem("email", email);

                alert(data.message || "Login realizado com sucesso!");

                window.location.href = "/Home/Index";  // 🔹 Redirecionamento correto para ASP.NET MVC
            } else {
                alert(data.message || "Erro ao fazer login.");
            }
        } catch (error) {
            console.error("Erro ao fazer login:", error);
            alert("Erro ao fazer login.");
        }
    });
});
