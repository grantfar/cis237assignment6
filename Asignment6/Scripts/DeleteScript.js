
function deleteFunction() {
    if($(confirm("Are You Sure")))
    {
        document.forms[1].submit();
    }
    else
    {
        alert("not deleted");
    }
}
