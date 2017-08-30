$(document).ready(function () {

    $('#tgtUnderTheHood').click(function () {
        $('#divUnderTheHood').slideToggle(1500);
    });


    $('#tgtWhatIsThis').click(function () {
        $('#divWhatIsThis').slideToggle(1500);
    });

    $('#lnkAddInventory').click(function () {
        $('#AddInventory').show(1000);
        $('#RemoveInventory').hide(1000);
    });

    $('#lnkRemoveInventory').click(function () {
        $('#RemoveInventory').show(1000);
        $('#AddInventory').hide(1000);
    });

    $('.dropdownPartName').change(function () {

        if ($('.dropdownPartName').val() === 'Other')
        {

            $("#addNewPartDiv").show(1000);
            
        }
        else {

            $("#addNewPartDiv").hide(1000);
            
        }
    });
 
});