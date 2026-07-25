var posibleOutputTypeList = new List<short?>() { 1, 2, 3, 4, 5, 8 };

if (posibleOutputTypeList.Contains(wood.OutputType)
{
    // docode...
}

// not contains
if (!posibleOutputTypeList.Contains(wood.OutputType))
{
    // docode...
}

/* ************************************************************************ */
var TransactionTypes = new List<int> { 1, 2, 3, 7, 10, 13, 15 };

IQueryable<UnreconciledBankTransactionQuery>
    query = (from bt in Context.BankTransactionEntities
             where bt.BankAccountId == input.BankAccountId && TransactionTypes.Contains(bt.TransactionType)
             orderby bt.TransactionDate, bt.BankTransactionId ascending
             select new UnreconciledBankTransactionQuery
             {
                 BankTransactionId = bt.BankTransactionId,
                 TransactionDate = bt.TransactionDate,
                 PayeeOrPayor = bt.PayeeOrPayor,
                 TransactionType = bt.TransactionType,
                 Description = bt.Description,
                 ClearedFlag = bt.ClearedFlag,

             });

/* *****************************FIND ID IN OBJECT CONTEXT******************************************* */
var undepositedFundids = undepositedTransactions.Select(s => s.UndepositedFundsId).ToList();
var undepositedFunds = context.UndepositedFundsEntities.Where(x => undepositedFundids.Contains(x.UndepositedFundsId)).ToList();


/* ********************************* FIND ELEMENTS AMONG TWO LISTS ********************************* */
List<Materia> materias = Contexto.Materias.Where(x => foliosMaterias.Any(m => m.MateriaId == x.MateriaId)).ToList();

/* *********CONTAINS************* */
List<int> pagosMesesAlumno = alumnoPago.Select(s => s.MesId).ToList();
var result = noPagos.Where(x => !pagosMesesAlumno.Contains(x)).ToList();