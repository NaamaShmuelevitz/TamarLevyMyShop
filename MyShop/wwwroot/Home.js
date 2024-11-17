﻿

const getLoginDataFromForm = () => {
    const userName = document.querySelector("#loginUserName").value;
    const password = document.querySelector("#loginPassword").value;
    return { userName, password };
}

const userLogIn = async () => {
    const { userName, password } = getLoginDataFromForm();
    try {
        const responsePost = await fetch(`api/users/login?userName=${userName}&password=${password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (responsePost.status == 204)
            alert("User Not Found!")
        else if (!responsePost.ok)
            alert("Error, Please try again")
        else {
            const data = await responsePost.json();
            alert(`${data.userName} logged in`);
            sessionStorage.setItem('user', JSON.stringify(data))
            window.location.href="./update.html"
            }
    }
    catch (error) {
        throw error;
    }
}

const toRegister = () => {
    const register = document.querySelector("#register");
    register.style.visibility = 'visible';
}

const getRegisterDataFromForm = () => { 
    const userName = document.querySelector("#userName").value;
    const password = document.querySelector("#password").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    return { userName, password, firstName, lastName };
}

const createUser = async () => {
    const user = getRegisterDataFromForm();
    try {
        const responsePost = await fetch('api/users', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        if (responsePost.status === 204)
            alert("weak password");
        if (!responsePost.ok)
            alert("Error, Please try again")
        const data = await responsePost.json();
            alert(`${data.userName} created`);
    }
    catch (error){
        throw error;
    }
}
const checkpassword = async () => {
    const password = document.querySelector("#password").value;
    const score = document.querySelector("#score");
    try {
        const responsePost = await fetch('api/users/password', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(password)
        });
        const data = await responsePost.json();
        score.value = data;
    }
    catch (error) {
        throw error;
    }
   
}