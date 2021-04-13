  
  describe('Testing Confirm Friend', () => {
    context('single value', () => {
      it('Confirm Friend', () => {
        cy.visit('http://localhost:3000/FriendsList')
        cy.get('[style="display: block; width: 20vw; height: 20vh; overflow-y: auto;"] > tbody > :nth-child(2) > :nth-child(2) > a').click()

        cy.get('.sortable').contains('TestUser10')


      })
    })
  })