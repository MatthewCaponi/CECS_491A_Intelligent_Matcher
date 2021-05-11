  
  describe('Testing Confirm Friend', () => {
    context('single value', () => {
      it('Confirm Friend', () => {


        cy.visit(Cypress.env('host') + '/Login')

        cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

        cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

        cy.get('.red').click()

        
        cy.visit('http://localhost:3000/FriendsList')


        cy.get(':nth-child(8) > tbody > :nth-child(2) > :nth-child(3) > a').click()

        cy.get('.sortable').contains('TestUser10')


      })
    })
  })