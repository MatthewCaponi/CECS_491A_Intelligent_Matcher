
  
  describe('Editing User Data', () => {
    context('single value', () => {
      it('EditData', () => {


        cy.visit(Cypress.env('host') + '/Login')

        cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

        cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

        cy.get('.red').click()




        cy.visit(Cypress.env('host') + '/profile?id=1')

        cy.get(':nth-child(3) > :nth-child(2) > .ui').click()

        cy.get('textarea').type(`Test descirption`)

        cy.get(':nth-child(2) > :nth-child(3) > div > :nth-child(2)').click()

        cy.get(':nth-child(2) > :nth-child(3) > div').contains('Account Information Updated')



      })
    })
  })