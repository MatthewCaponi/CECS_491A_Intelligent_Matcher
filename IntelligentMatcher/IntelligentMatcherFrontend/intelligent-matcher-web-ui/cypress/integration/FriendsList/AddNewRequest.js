describe('Testing Adding New Friend', () => {
  context('single value', () => {
    it('Confirm Friend', () => {


      cy.visit(Cypress.env('host') + '/Login')

      cy.get(':nth-child(5) > .ui > input').type(`TestUser1`)

      cy.get(':nth-child(8) > .ui > input').type(`TestPassword1`)

      cy.get('.red').click()

      
      cy.visit(global.urlRoute + 'FriendsList')


      cy.get(':nth-child(1) > input').type(`TestUser21`)

      cy.get('.box > :nth-child(2) > .button').click()

      cy.get(':nth-child(2) > :nth-child(9)').contains('TestUser21')


    })
  })
})