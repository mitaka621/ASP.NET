function GetNumber() {
    let num = document.querySelector("#numInput").value;

    window.location = "https://localhost:7062/Home/NumberstoN?count=" + num;
}