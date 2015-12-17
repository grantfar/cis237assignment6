function validateInput()
{
    
    if (!isNaN($("#price").val()) && isEmptyOrBlank($("#name").val()) && isEmptyOrBlank($("#pack").val()) && isEmptyOrBlank($("#id").val()))
    {
        document.forms[1].submit();
    }
    else
    {
        alert("bad input")
    }
}

function isEmptyOrBlank  (testString)
{
    return !(testString.trim().length === 0);
}