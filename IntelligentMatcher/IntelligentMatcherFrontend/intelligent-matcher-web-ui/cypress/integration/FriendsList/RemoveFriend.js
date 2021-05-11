  
  describe('Testing Remove Friend', () => {
    context('single value', () => {
      it('Remove Friend', () => {


        cy.visit(Cypress.env('host') + '/Login')

        cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

        cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

        cy.get('.red').click()

        
        cy.visit(global.urlRoute + 'FriendsList')


        cy.get('.sortable > tbody > :nth-child(2) > :nth-child(3) > a').click()

        cy.get('.sortable').contains('TestUser2').should('not.exist')


      })
    })
  })