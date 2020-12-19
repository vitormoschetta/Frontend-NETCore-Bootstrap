
function Filter(filtro, rota) {
    $.post(rota, { filtro: filtro }, function (data) {
        $("#tabela-index").empty();
        $("#tabela-index").html(data);
    })

}


function Pagination(pageNumber, filtro, rota) {
    $.post(rota, { filtro: filtro, pageNumber: pageNumber }, function (data) {
        $("#tabela-index").empty();
        $("#tabela-index").html(data);
    })
}



//////////////////////////// VALIDA CEP ///////////////////////////////////////

function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('Rua').value = ("");
    document.getElementById('Bairro').value = ("");
    document.getElementById('Cidade').value = ("");
    document.getElementById('Uf').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('Rua').value = (conteudo.logradouro);
        document.getElementById('Bairro').value = (conteudo.bairro);
        document.getElementById('Cidade').value = (conteudo.localidade);
        document.getElementById('Uf').value = (conteudo.uf);
    } //end if.
    else {
        //CEP não Encontrado.
        limpa_formulário_cep();
        alert("CEP não encontrado.");
    }
}

function pesquisacep(valor) {

    //Nova variável "cep" somente com dígitos.
    var cep = valor.replace(/\D/g, '');

    //Verifica se campo cep possui valor informado.
    if (cep != "") {

        //Expressão regular para validar o CEP.
        var validacep = /^[0-9]{8}$/;

        //Valida o formato do CEP.
        if (validacep.test(cep)) {
            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('Rua').value = "...";
            document.getElementById('Bairro').value = "...";
            document.getElementById('Cidade').value = "...";
            document.getElementById('Uf').value = "...";

            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

            //Insere script no documento e carrega o conteúdo.
            document.body.appendChild(script);

        } //end if.
        else {
            //cep é inválido.
            limpa_formulário_cep();
            alert("Formato de CEP inválido.");
        }
    } //end if.
    else {
        //cep sem valor, limpa formulário.
        limpa_formulário_cep();
    }
};


/* 
function ConsultaCep(params, rota){

    var url = $("#"+rota).data("url"); 

    $.get(url, {cep: params}, function (data)
    {            
        var rua = data.logradouro;
        var bairro = data.bairro;        
        var cidade = data.cidade;
        var uf = data.uf;
        // removendo parte do texto logradouro:
        //var index = rua.indexOf("-");
        //rua = rua.substring(0, index);

        $("#Rua").val(rua);
        $("#Bairro").val(bairro);
        $("#Cidade").val(cidade);
        $("#Uf").val(uf);
        
    })
    .fail(function() {
        alert("Erro interno.");
    }); 
}
*/



//////////////////////////////// VALIDA FORMULARIO /////////////////////////////////////

/* Config Vars */
/* Nao alterar ValidateState */
validateState = false;

/* Required message */
validateRequiredMsg = "Campo de preechimento obrigatório";
/* E-mail message */
validateMailMsg = "E-mail informado é inválido";
/* Numeric message */
validateNumericMsg = "O valor deve ser numérico";
/* Min message */
validateMinMsg = "A quantidade mínima de caracters é: ";
/* Max message */
validateMaxMsg = "A quantidade máxima de caracteres é: ";
/* Password message */
validatePasswordMsg = "Senhas não conferem";


function validate(form_id) {
    $('#' + form_id + ' :input').each(function () {
        /* required */
        if ($(this).hasClass('required') && $.trim($(this).val()) == "") {
            $(this).addClass('invalid');
            $(this).focus();
            document.querySelector('#validate_message').value = validateRequiredMsg;
            validateState = false;
            return false;
        }
        else {
            $(this).removeClass('invalid');
            validateState = true;
        }

        /* numeric value */
        if ($(this).hasClass('required') && $(this).hasClass('numeric')) {
            var nan = new RegExp(/(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/);
            if (!nan.test($.trim($(this).val()))) {
                $(this).addClass('invalid');
                $(this).focus();
                document.querySelector('#validate_message').value = validateNumericMsg;
                validateState = false;
                return false;
            }
            else {
                $('#validate_message').html('');
                $(this).removeClass('invalid');
                validateState = true;
            }
        }

        /* mail */
        if ($(this).hasClass('email')) {
            var er = new RegExp(/^[A-Za-z0-9_\-\.]+@[A-Za-z0-9_\-\.]{2,}\.[A-Za-z0-9]{2,}(\.[A-Za-z0-9])?/);
            if (!er.test($.trim($(this).val()))) {
                $(this).addClass('invalid');
                $(this).focus();
                document.querySelector('#validate_message').value = validateMailMsg;
                validateState = false;
                return false;
            }
            else {
                $(this).removeClass('invalid');
                validateState = true;
            }
        }

        /* min value */
        if ($(this).attr('min') && $(this).hasClass('required')) {
            if ($.trim($(this).val()).length < $(this).attr('min')) {
                $(this).addClass('invalid');
                $(this).focus();
                document.querySelector('#validate_message').value = validateMinMsg + $(this).attr('min');
                validateState = false;
                return false;
            }
            else {
                $('#validate_message').html('');
                $(this).removeClass('invalid');
                validateState = true;
            }
        }

        /* max value */
        if ($(this).attr('max') && $(this).hasClass('required')) {
            if ($.trim($(this).val()).length > $(this).attr('max')) {
                $(this).addClass('invalid');
                $(this).focus();
                document.querySelector('#validate_message').value = validateMaxMsg + $(this).attr('max');
                validateState = false;
                return false;
            }
            else {
                $('#validate_message').html('');
                $(this).removeClass('invalid');
                validateState = true;
            }
        }
        /* password */
        if ($(this).hasClass('password') && $(this).nextAll('.password').hasClass('password')) {
            if ($.trim($(this).val()) != $.trim($(this).nextAll('.password').val())) {
                $(this).nextAll('.password').addClass('invalid');
                $(this).nextAll('.password').focus();
                document.querySelector('#validate_message').value = validatePasswordMsg;
                validateState = false;
                return false;
            }
            else {
                $('#validate_message').html('');
                $(this).nextAll('.password').removeClass('invalid');
                validateState = true;
            }
        }
    })
}
