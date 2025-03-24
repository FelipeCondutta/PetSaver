const container = document.querySelector(".container");
const registerBtn = document.querySelector(".register-btn");
const loginBtn = document.querySelector(".login-btn");

registerBtn.addEventListener("click", () => {
    container.classList.add("active");
});

loginBtn.addEventListener("click", () => {
    container.classList.remove("active");
});

document.querySelector(".register form").addEventListener("submit", async (e) => {
    e.preventDefault();

    const name = document.querySelector(".register input[name='username']").value;
    const email = document.querySelector(".register input[name='email']").value;
    const password = document.querySelector(".register input[name='password']").value;

    try {
        const response = await fetch("http://localhost:3000/api/users/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ name: name, email, password: password }),
        });

        const data = await response.json();


        if (response.ok) {
            container.classList.remove("active");
        }
    } catch (error) {
        console.error("Erro ao cadastrar:", error);
        alert("Erro ao cadastrar usuário.");
    }
});

document.querySelector(".login form").addEventListener("submit", async (e) => {
    e.preventDefault();

    const email = document.querySelector(".login input[name='email']").value;
    const password = document.querySelector(".login input[name='password']").value;

    try {
        const response = await fetch("http://localhost:3000/api/users/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password: password }),
        });

        const data = await response.json();

        if (response.ok) {
            localStorage.setItem("token", data.token);
            localStorage.setItem("email", email);
            window.location.href = "TelaPrincipal\\index.html";
            alert(data.message);
        }
    } catch (error) {
        console.error("Erro ao fazer login:", error);
        alert("Erro ao fazer login.");
    }
});

document.addEventListener("DOMContentLoaded", () => {

    const token = localStorage.getItem("token");

    const loginBtn = document.querySelector("#loginBtn");
    const accountBtn = document.querySelector("#accountBtn");

    if (token) {

        loginBtn.style.display = "none";
        accountBtn.style.display = "block";
    } else {

        loginBtn.style.display = "block";
        accountBtn.style.display = "none";
    }
});
