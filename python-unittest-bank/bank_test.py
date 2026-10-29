# Emil Abdumalikov ID: 
import unittest
import bank


class TestBank(unittest.TestCase):
    def setUp(self):
        "Initial setup for the tests"
        bank.bank_accounts_money = [100, 0, 0, 0]

    # at least 3 tests for check_not_negative_or_zero
    def test1_check_not_negative_or_zero(self):
        "Test for positive number"
        x = 1
        self.assertTrue(bank.check_not_negative_or_zero(x))

    def test2_check_not_negative_or_zero(self):
        "Test for 0"
        x = 0
        self.assertFalse(bank.check_not_negative_or_zero(x))

    def test3_check_not_negative_or_zero(self):
        "Test for negative number"
        x = -1
        self.assertFalse(bank.check_not_negative_or_zero(x))

    # at least 2 tests for convert_to_number
    def test1_convert_to_number(self):
        "Test for float number"
        x = 100.99
        self.assertEqual(bank.convert_to_number(x), 100)

    def test2_convert_to_number(self):
        "Test for None"
        x = None
        self.assertIsNone(bank.convert_to_number(x), "None")

    # at least 2 tests for deposit
    def test1_deposit(self):
        "Test for depositing money to the account"
        deposit_amount = 100
        bank.deposit(deposit_amount)
        self.assertEqual(bank.bank_accounts_money[0], 199)

    def test2_deposit(self):
        "Test for depositing None"
        deposit_amount = None
        bank.deposit(deposit_amount)
        self.assertIsNotNone(bank.bank_accounts_money[0], None)

    # at least 3 tests for withdraw
    def test1_withdraw(self):
        "Test for withdrawing money from the account"
        initial_balance = bank.bank_accounts_money[0]
        withdrawl_amount = 60
        bank.withdraw(withdrawl_amount)  # 60 - 1
        self.assertEqual(bank.bank_accounts_money[0], initial_balance - withdrawl_amount - bank.FEE)

    def test2_withdraw(self):
        "Test for withdrawing more than the balance"
        initial_balance = bank.bank_accounts_money[0]
        withdrawl_amount = 200
        bank.withdraw(withdrawl_amount)
        self.assertEqual(bank.bank_accounts_money[0], initial_balance)

    def test3_withdraw(self):
        "Test for withdrawing 0$"
        initial_balance = bank.bank_accounts_money[0]
        withdrawl_amount = 0
        bank.withdraw(withdrawl_amount)
        self.assertEqual(bank.bank_accounts_money[0], initial_balance)

    # at least 4 tests for transfer
    def test1_transfer(self):
        "Test for transferring money to another account"
        initial_balance = bank.bank_accounts_money[0]
        transfer_amount = 60
        to_account = 1
        bank.transfer(to_account, transfer_amount)
        self.assertEqual(bank.bank_accounts_money[0], initial_balance - transfer_amount - bank.FEE)
        self.assertEqual(bank.bank_accounts_money[1], transfer_amount)

    def test2_transfer(self):
        "Test for transferring money to an account that does not exist"
        total_before = sum(bank.bank_accounts_money)
        transfer_amount = 60
        to_account = 0
        bank.transfer(to_account, transfer_amount)
        total_after = sum(bank.bank_accounts_money)
        self.assertEqual(total_before, total_after,
                         "Transfer failed")  # there is a bug in the code because of the fee of 1$
        # supposed to be 1$ more than the total before

    def test3_transfer(self):
        "Test for transferring more money than the balance in the account"
        initial_balance = bank.bank_accounts_money[0]
        transfer_amount = "200"
        to_account = 1
        bank.transfer(to_account, transfer_amount)
        self.assertEqual(bank.bank_accounts_money[0], initial_balance)

    def test4_transfer(self):
        "Test for transferring 0$"
        initial_balance = bank.bank_accounts_money[0]
        transfer_amount = 60
        to_account = 10
        bank.transfer(to_account, transfer_amount)
        self.assertEqual(bank.bank_accounts_money[0], initial_balance)

    # at least 2 tests for withdraw_check_balance
    def test1_withdraw_check_balance(self):
        "Test for withdrawing money from the account"
        amount = 60
        self.assertTrue(bank.withdraw_check_balance(amount))

    def test2_withdraw_check_balance(self):
        "Test for withdrawing more than the balance in the account"
        amount = 200
        self.assertFalse(bank.withdraw_check_balance(amount))
