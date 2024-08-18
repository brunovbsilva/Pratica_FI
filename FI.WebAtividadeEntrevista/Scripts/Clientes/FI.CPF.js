$('#CPF').on('input', function () {
    var value = $(this).val();
    $(this).val(formatCPF(value));
});

function formatCPF(value) {
    value = value.replace(/\D/g, '');

    if (value.length > 9) {
        value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
    } else if (value.length > 6) {
        value = value.replace(/(\d{3})(\d{3})(\d{3})/, '$1.$2.$3');
    } else if (value.length > 3) {
        value = value.replace(/(\d{3})(\d{3})/, '$1.$2');
    }
    return value;
}

function formatBeneficiariosCPF(beneficiarios) {
    beneficiarios.forEach(beneficiario => beneficiario.CPF = formatCPF(beneficiario.CPF));
}

function validateCPF(CPF) {
    var aggregate = 0;
    CPF = CPF.replace(/[^0-9]/g, '');

    if (CPF == "00000000000") return false;

    for (i = 1; i <= 9; i++) aggregate = aggregate + parseInt(CPF.substring(i - 1, i)) * (11 - i);
    var rest = (aggregate * 10) % 11;

    if ((rest == 10) || (rest == 11)) rest = 0;
    if (rest != parseInt(CPF.substring(9, 10))) return false;

    aggregate = 0;
    for (i = 1; i <= 10; i++) aggregate = aggregate + parseInt(CPF.substring(i - 1, i)) * (12 - i);
    rest = (aggregate * 10) % 11;

    if ((rest == 10) || (rest == 11)) rest = 0;
    if (rest != parseInt(CPF.substring(10, 11))) return false;
    return true;
}