  
  describe('Testing Cancel Friend Request', () => {
    context('single value', () => {
      it('Remove Request', () => {


        cy.visit(Cypress.env('host') + '/Login')

        cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

        cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

        cy.get('.red').click()

        
        cy.visit(global.urlRoute + 'FriendsList')


        cy.get(':nth-child(9) > tbody > :nth-child(2) > :nth-child(3) > a').click()

        cy.get(':nth-child(2) > :nth-child(9)').contains('TestUser18').should('not.exist')


      })
    })
  })