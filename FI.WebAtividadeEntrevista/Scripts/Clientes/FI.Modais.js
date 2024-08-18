let random = Math.random().toString().replace('.', '');
let init = true;
let invalidCPFMessage = 'Já existe um beneficiário com este CPF na lista.';
$(document).on('submit', '#benForm', function (event) {
    event.preventDefault();
    var formData = new FormData(this);

    var index = formData.get('index-id-control');
    var benName = formData.get('ben-name');
    var benCPF = formData.get('ben-cpf');

    if (!validateCPF(benCPF)) {
        alert('CPF invalido');
        return;
    }

    if(index >= 0) updateBen(benCPF, benName, index);
    else pushBen(benCPF, benName);
    
    $('#index-id-control').val(-1);
    $('#benForm')[0].reset();
    getBeneficiarios();
    $('#form-include-btn').text('Incluir');
});

function updateBen(cpf, name, index) {
    if (beneficiarios.findIndex(x => formatCPF(x.CPF) == formatCPF(cpf) && x.Id !== beneficiarios[index].Id) >= 0)
        alert(invalidCPFMessage);
    else {
        beneficiarios[index].Nome = name;
        beneficiarios[index].CPF = cpf;
    }
}

function pushBen(cpf, name) {
    if (beneficiarios.findIndex(x => formatCPF(x.CPF) == formatCPF(cpf)) >= 0)
        alert(invalidCPFMessage);
    else beneficiarios.push({ Id: 0, Nome: name, CPF: cpf });
}

$('#Beneficiarios').on('click', function () {
    createModalBeneficiarios();
    getBeneficiarios();
});

function ModalDialog(titulo, texto) {
    var id = Math.random().toString().replace('.', '');
    var texto = `
        <div id="${id}" class="modal fade">
            <div class="modal-dialog">                                                                                 
                <div class="modal-content">                                                                            
                    <div class="modal-header">                                                                         
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         
                        <h4 class="modal-title">${titulo}</h4>                                                    
                    </div>                                                                                             
                    <div class="modal-body">                                                                           
                        <p>${texto}</p>                                                                           
                    </div>                                                                                             
                    <div class="modal-footer">                                                                         
                        <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>            
                    </div>                                                                                             
                </div>
            </div> 
        </div>
    `;

    $('body').append(texto);
    $('#' + id).modal('show');
}
function createModalBeneficiarios() {
    var title = 'Beneficiários';
    var texto = `
        <div id="${random}" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">${title}</h4>  
                    </div>
                    <div class="modal-body">
                        ${getBeneficiariosForm()}
                        ${getBeneficiariosTable()}
                    </div>
                </div>
            </div>
        </div>
    `;

    $('body').append(texto);

    $('#' + random).on('hidden.bs.modal', function () {
        $('#benForm')[0].reset();
    });

    $('#ben-cpf').on('input', function () {
        var value = $(this).val();
        $(this).val(formatCPF(value));
    });

    $(`#${random}`).modal('show');
}
function getBeneficiariosForm() {
    return `
        <form id="benForm">
            <input type="hidden" class="form-control" id="index-id-control" name="index-id-control" value="-1">
            <div class="form-container">
                <div class="form-group form-container__cpf">
                    <label for="CPF">CPF:</label>
                    <input required="required" type="text" class="form-control" id="ben-cpf" name="ben-cpf" placeholder="Ex.: 010.011.111-00" maxlength="14">
                </div>
                <div class="form-group form-container__name">
                    <label for="Nome">Nome:</label>
                    <input required="required" type="text" class="form-control" id="ben-name" name="ben-name" placeholder="Ex.: Maria" maxlength="50">
                </div>
                <div class="form-group form-container__actions">
                    <button type="submit" id="form-include-btn" class="btn btn-success include-btn-modal">Incluir</button>
                </div>
            </div>
        </form>
    `;
}
function getBeneficiariosTable() {
    var actions = 'Ações';
    return `
        <table id="ben-table" class="table">
            <thead>
                <tr>
                    <th scope="col" class="cpf-column">CPF</th>
                    <th scope="col">Nome</th>
                    <th scope="col" class="action-column">${actions}</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    `;
}
function getBeneficiarios() {
    if (init) {
        $.ajax({
            url: urlBeneficiarios,
            type: 'get',
            async: false,
            success: data => {
                beneficiarios = data;
                init = false;
            },
            error: error => { 
                console.error(data);
            }
        });
    }
    checkBeneficiariosTableValues();
    appendBeneficiariosTableBody();
}
function appendBeneficiariosTableBody() {
    let table_body = $('#ben-table tbody');
    table_body.empty();
    let result = '';
    beneficiarios.forEach(beneficiario => result = result + addBeneficiario(beneficiario));
    table_body.append(result);
}
function checkBeneficiariosTableValues() {
    if(!beneficiarios.length) $('#ben-table').addClass('ocult-table')
    else $('#ben-table').removeClass('ocult-table')
}
function addBeneficiario(beneficiario) {
    return `
        <tr>
            <td>${formatCPF(beneficiario.CPF)}</td>
            <td>${beneficiario.Nome}</td>
            <td>
                <button type="button" class="btn btn-primary btn-sm" onclick="updateBeneficiario(${beneficiario.Id})">Alterar</button>
                <button type="button" class="btn btn-primary btn-sm" onclick="deleteBeneficiario(${beneficiario.Id})">Excluir</button>
            </td>
        </tr>
    `;
}
function updateBeneficiario(id) {
    var index = beneficiarios.findIndex((b) => b.Id == id);
    $('#index-id-control').val(index);
    $('#ben-name').val(beneficiarios[index].Nome);
    $('#ben-cpf').val(beneficiarios[index].CPF);
    $('#form-include-btn').text('Alterar');
}
function deleteBeneficiario(id) {
    var index = beneficiarios.findIndex(x => x.Id == id);
    beneficiarios.splice(index, 1);
    beneficiarios.sort((a, b) => a.Nome.localeCompare(b.Nome));
    getBeneficiarios();
}