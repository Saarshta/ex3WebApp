document.getElementById('name').value = localStorage.getItem("mazeName");
document.getElementById('rows').value = localStorage.getItem("rows");
document.getElementById('cols').value = localStorage.getItem("cols");
document.getElementById('ip').value = localStorage.getItem("ip");
document.getElementById('port').value = localStorage.getItem("port");
if (localStorage.getItem("algorithm") != null) {
    document.getElementById('searchAlgo').value = localStorage.getItem("algorithm");
}
